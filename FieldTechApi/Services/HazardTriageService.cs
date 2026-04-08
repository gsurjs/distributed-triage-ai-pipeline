// ==============================================================================
// INTEGRATION: OpenAI Hazard Triage Service (Standard API)
// PURPOSE: Takes a field report description, queries the standard OpenAI model, 
//          and parses the response into a Risk Level and Recommended Action.
// ==============================================================================

using OpenAI;
using OpenAI.Chat;

namespace FieldTechApi.Services;

public class HazardTriageService
{
    private readonly ChatClient _chatClient;

    public HazardTriageService(IConfiguration config)
    {
        // Pull the single key from your .env file
        var apiKey = config["OpenAI:ApiKey"];

        // Initialize the standard OpenAI client
        OpenAIClient client = new(apiKey);
        
        // Target the gpt-4o model directly
        _chatClient = client.GetChatClient("gpt-4o");
    }

    public async Task<(string RiskLevel, string RecommendedAction)> AnalyzeHazardAsync(string description)
    {
        var systemPrompt = @"You are a construction safety expert AI. 
Analyze the following field report description. 
You must respond ONLY in this exact format: [Risk Level]|[Recommended Action]
Risk levels must be: Low, Medium, or Critical.
Example: Critical|Immediately shut off main power and evacuate sector.";

        ChatCompletion completion = await _chatClient.CompleteChatAsync(
            [
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(description)
            ]);

        var responseText = completion.Content[0].Text;
        var parts = responseText.Split('|');

        if (parts.Length == 2)
        {
            return (parts[0].Trim(), parts[1].Trim());
        }

        return ("Unknown", "Failed to parse AI response.");
    }
}