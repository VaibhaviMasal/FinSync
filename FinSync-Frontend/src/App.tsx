import { useEffect, useState } from "react";
import axios from "axios";

function App() {
  const [summary, setSummary] = useState<any>(null);

  useEffect(() => {
    axios.get("http://localhost:5221/api/dashboard/summary")
      .then(res => {
        console.log(res.data);
        setSummary(res.data);
      })
      .catch(err => console.error(err));
  }, []);

  return (
    <div style={{ padding: "20px", fontFamily: "Arial" }}>
      <h1>FinSync Dashboard</h1>

      {!summary ? (
        <p>Loading...</p>
      ) : (
        <div>
          <h2>Total Customers: {summary.totalCustomers}</h2>
          <h2>Total Policies: {summary.totalPolicies}</h2>
          <h2>Total Premium: ₹ {summary.totalPremium}</h2>
        </div>
      )}
    </div>
  );
}

export default App;