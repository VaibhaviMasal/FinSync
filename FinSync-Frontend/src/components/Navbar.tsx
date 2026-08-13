import { Link, useLocation } from "react-router-dom";

const Navbar = () => {
  const location = useLocation();

  const linkStyle = (path: string) => ({
    textDecoration: "none",
    color: location.pathname === path ? "#4ade80" : "white",
    fontWeight: "bold",
  });

  return (
    <nav
      style={{
        backgroundColor: "#111827",
        padding: "15px 30px",
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
      }}
    >
      <h2 style={{ color: "white" }}>FinSync CRM</h2>

      <div style={{ display: "flex", gap: "20px" }}>
        <Link to="/" style={linkStyle("/")}>
          Dashboard
        </Link>

        <Link to="/customers" style={linkStyle("/customers")}>
          Customers
        </Link>

        <Link to="/policies" style={linkStyle("/policies")}>
          Policies
        </Link>
      </div>
    </nav>
  );
};

export default Navbar;