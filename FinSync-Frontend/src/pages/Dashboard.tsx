import { useEffect, useState } from "react";
import api from "../services/api";

interface DashboardData {
  totalCustomers: number;
  totalPolicies: number;
  activePolicies: number;
  totalRevenue: number;
  pendingRenewals: number;
}

const Dashboard = () => {
  return (
    <div style={{ padding: "30px" }}>
      <h1>Dashboard</h1>
      <p>Welcome to FinSync CRM 🚀</p>

      <div style={{ marginTop: "20px" }}>
        <div
          style={{
            padding: "20px",
            background: "#f3f4f6",
            borderRadius: "10px",
            marginBottom: "10px",
          }}
        >
          Total Customers: (connect later)
        </div>

        <div
          style={{
            padding: "20px",
            background: "#f3f4f6",
            borderRadius: "10px",
          }}
        >
          Total Policies: (coming soon)
        </div>
      </div>
    </div>
  );
};

export default Dashboard;