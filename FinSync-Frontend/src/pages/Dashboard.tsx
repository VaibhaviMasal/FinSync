import { useEffect, useState } from "react";
import api from "../services/api";

interface DashboardData {
  totalCustomers: number;
  totalPolicies: number;
  activePolicies: number;
  totalRevenue: number;
  pendingRenewals: number;
}

function Dashboard() {
  const [data, setData] = useState<DashboardData | null>(null);

  useEffect(() => {
    api.get("/dashboard/summary")
      .then(res => setData(res.data))
      .catch(err => console.error(err));
  }, []);

  if (!data) return <p>Loading...</p>;

  return (
    <div style={{ padding: "20px" }}>
      <h2>Dashboard</h2>

      <div style={{ display: "flex", gap: "20px", marginTop: "20px" }}>
        
        <div style={cardStyle}>
          <h3>Customers</h3>
          <p>{data.totalCustomers}</p>
        </div>

        <div style={cardStyle}>
          <h3>Policies</h3>
          <p>{data.totalPolicies}</p>
        </div>

        <div style={cardStyle}>
          <h3>Active</h3>
          <p>{data.activePolicies}</p>
        </div>

        <div style={cardStyle}>
          <h3>Revenue</h3>
          <p>₹ {data.totalRevenue}</p>
        </div>

        <div style={cardStyle}>
          <h3>Renewals</h3>
          <p>{data.pendingRenewals}</p>
        </div>

      </div>
    </div>
  );
}

const cardStyle = {
  padding: "20px",
  border: "1px solid #ddd",
  borderRadius: "10px",
  width: "150px",
  textAlign: "center" as const
};

export default Dashboard;