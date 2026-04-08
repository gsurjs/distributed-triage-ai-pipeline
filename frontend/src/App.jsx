// ==============================================================================
// INTEGRATION: React Dashboard
// PURPOSE: Fetches FieldReports from the API on mount and renders them using 
//          Tailwind CSS utility classes for styling.
// ==============================================================================

import { useState, useEffect } from 'react'

function App() {
  const [reports, setReports] = useState([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    // Because of our Vite proxy, we just call '/api/reports'
    fetch('/api/reports')
      .then(res => res.json())
      .then(data => {
        setReports(data)
        setLoading(false)
      })
      .catch(err => console.error("Failed to fetch reports:", err))
  }, [])

  return (
    <div className="min-h-screen bg-gray-100 p-8">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-3xl font-bold text-gray-800 mb-8">Field Tech Dashboard</h1>
        
        {loading ? (
          <p className="text-gray-600">Loading reports...</p>
        ) : (
          <div className="grid gap-4 md:grid-cols-2">
            {reports.map(report => (
              <div key={report.id} className="bg-white p-6 rounded-lg shadow-md border-l-4 border-blue-500">
                <div className="flex justify-between items-start mb-2">
                  <h2 className="text-xl font-semibold text-gray-800">{report.title}</h2>
                  <span className={`px-2 py-1 text-xs font-bold rounded ${
                    report.riskLevel === 'Critical' ? 'bg-red-100 text-red-800' : 
                    report.riskLevel === 'Medium' ? 'bg-yellow-100 text-yellow-800' : 
                    'bg-green-100 text-green-800'
                  }`}>
                    {report.riskLevel || 'Pending'}
                  </span>
                </div>
                
                <p className="text-gray-600 mb-4 text-sm">{report.description}</p>
                
                {report.aiRecommendedAction && (
                  <div className="bg-blue-50 p-3 rounded text-sm mb-4">
                    <span className="font-bold text-blue-800 block mb-1">AI Recommendation:</span>
                    <span className="text-blue-900">{report.aiRecommendedAction}</span>
                  </div>
                )}
                
                <div className="flex justify-between text-xs text-gray-500">
                  <span>Logged by: {report.loggedBy}</span>
                  <span>{new Date(report.createdAt).toLocaleDateString()}</span>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  )
}

export default App