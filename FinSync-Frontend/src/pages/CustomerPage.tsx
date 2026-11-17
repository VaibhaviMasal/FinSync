import { useEffect, useState } from "react";
import {
  getCustomers,
  createCustomer,
  deleteCustomer,
  updateCustomer,
} from "../api/customerApi";

interface Customer {
  customerId?: number;
  firstName: string;
  lastName: string;
  gender?: number;
  dateOfBirth: string;
  mobileNumber: string;
  email: string;
  address: string;
  city: string;
  state: string;
  pincode: string;
  panNumber: string;
  aadhaarNumber: string;
}

const CustomerPage = () => {
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(false);
  const [editingId, setEditingId] = useState<number | null>(null);

  const [formData, setFormData] = useState<Customer>({
    firstName: "",
    lastName: "",
    gender: 0,
    dateOfBirth: "",
    mobileNumber: "",
    email: "",
    address: "",
    city: "",
    state: "",
    pincode: "",
    panNumber: "",
    aadhaarNumber: "",
  });

  // ---------------- LOAD CUSTOMERS ----------------
  const loadCustomers = async () => {
    try {
      const response = await getCustomers();

      if (response?.success && Array.isArray(response.data)) {
        setCustomers(response.data);
      } else {
        setCustomers([]);
      }
    } catch (error) {
      console.error("Error loading customers:", error);
      setCustomers([]);
    }
  };

  useEffect(() => {
    loadCustomers();
  }, []);

  // ---------------- HANDLE INPUT ----------------
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: name === "gender" ? Number(value) : value,
    }));
  };

  // ---------------- SUBMIT ----------------
  const handleSubmit = async () => {
    try {
      setLoading(true);

      if (editingId) {
        await updateCustomer(editingId, formData);
        alert("Customer Updated ✅");
      } else {
        await createCustomer(formData);
        alert("Customer Added ✅");
      }

      setEditingId(null);

      setFormData({
        firstName: "",
        lastName: "",
        gender: 0,
        dateOfBirth: "",
        mobileNumber: "",
        email: "",
        address: "",
        city: "",
        state: "",
        pincode: "",
        panNumber: "",
        aadhaarNumber: "",
      });

      await loadCustomers();
    } catch (error) {
      console.error(error);
      alert("Operation failed ❌");
    } finally {
      setLoading(false);
    }
  };

  // ---------------- DELETE ----------------
  const handleDelete = async (id: number) => {
    try {
      await deleteCustomer(id);
      alert("Deleted 🗑️");
      await loadCustomers();
    } catch (error) {
      console.error(error);
      alert("Delete failed ❌");
    }
  };

  // ---------------- EDIT ----------------
  const handleEdit = (customer: Customer) => {
    setEditingId(customer.customerId!);

    setFormData({
      firstName: customer.firstName,
      lastName: customer.lastName,
      gender: customer.gender || 0,
      dateOfBirth: customer.dateOfBirth,
      mobileNumber: customer.mobileNumber,
      email: customer.email,
      address: customer.address,
      city: customer.city,
      state: customer.state,
      pincode: customer.pincode,
      panNumber: customer.panNumber,
      aadhaarNumber: customer.aadhaarNumber,
    });
  };

  // ---------------- SEARCH ----------------
  const filteredCustomers = customers.filter((c) => {
    const keyword = search.toLowerCase();

    return (
      c.firstName.toLowerCase().includes(keyword) ||
      c.lastName.toLowerCase().includes(keyword) ||
      c.mobileNumber.includes(keyword) ||
      c.city.toLowerCase().includes(keyword)
    );
  });

  // ---------------- STYLES ----------------
  const thStyle = {
    padding: "12px",
    textAlign: "left" as const,
    borderBottom: "2px solid #ddd",
  };

  const tdStyle = {
    padding: "10px",
    borderBottom: "1px solid #eee",
  };

  const editBtn = {
    backgroundColor: "#2563eb",
    color: "white",
    border: "none",
    padding: "6px 12px",
    borderRadius: "5px",
    cursor: "pointer",
  };

  const deleteBtn = {
    backgroundColor: "#dc2626",
    color: "white",
    border: "none",
    padding: "6px 12px",
    borderRadius: "5px",
    cursor: "pointer",
  };

  return (
    <div style={{ padding: "30px", maxWidth: "1100px", margin: "0 auto" }}>
      <h1>Customer Management</h1>

      {/* FORM */}
      <h2>{editingId ? "Edit Customer" : "Create Customer"}</h2>

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "repeat(3, 1fr)",
          gap: "12px",
        }}
      >
        <input name="firstName" placeholder="First Name" value={formData.firstName} onChange={handleChange} />
        <input name="lastName" placeholder="Last Name" value={formData.lastName} onChange={handleChange} />
        <input name="dateOfBirth" type="date" value={formData.dateOfBirth} onChange={handleChange} />

        <select name="gender" value={formData.gender} onChange={handleChange}>
          <option value={0}>Select Gender</option>
          <option value={1}>Male</option>
          <option value={2}>Female</option>
          <option value={3}>Other</option>
        </select>

        <input name="mobileNumber" placeholder="Mobile Number" value={formData.mobileNumber} onChange={handleChange} />
        <input name="email" placeholder="Email" value={formData.email} onChange={handleChange} />

        <input name="city" placeholder="City" value={formData.city} onChange={handleChange} />
        <input name="state" placeholder="State" value={formData.state} onChange={handleChange} />
        <input name="pincode" placeholder="Pincode" value={formData.pincode} onChange={handleChange} />

        <input name="panNumber" placeholder="PAN" value={formData.panNumber} onChange={handleChange} />
        <input name="aadhaarNumber" placeholder="Aadhaar Number" value={formData.aadhaarNumber} onChange={handleChange} />
        <input name="address" placeholder="Address" value={formData.address} onChange={handleChange} />
      </div>

      <button
        onClick={handleSubmit}
        disabled={loading}
        style={{
          marginTop: "20px",
          padding: "12px",
          width: "100%",
          backgroundColor: loading ? "gray" : editingId ? "orange" : "green",
          color: "white",
          border: "none",
          borderRadius: "5px",
        }}
      >
        {loading
          ? "Processing..."
          : editingId
          ? "Update Customer"
          : "Add Customer"}
      </button>

      {/* SEARCH */}
      <input
        type="text"
        placeholder="Search by name, mobile, city..."
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        style={{
          marginTop: "30px",
          padding: "10px",
          width: "100%",
          borderRadius: "5px",
          border: "1px solid #ccc",
        }}
      />

      {/* TABLE */}
      <h2 style={{ marginTop: "20px" }}>Customer List</h2>

      {filteredCustomers.length === 0 ? (
        <p>No customers found.</p>
      ) : (
        <div style={{ marginTop: "20px", overflowX: "auto" }}>
          <table
            style={{
              width: "100%",
              borderCollapse: "collapse",
              backgroundColor: "white",
            }}
          >
            <thead style={{ backgroundColor: "#f3f4f6" }}>
              <tr>
                <th style={thStyle}>Name</th>
                <th style={thStyle}>Mobile</th>
                <th style={thStyle}>Email</th>
                <th style={thStyle}>City</th>
                <th style={thStyle}>Actions</th>
              </tr>
            </thead>

            <tbody>
              {filteredCustomers.map((c, index) => (
                <tr
                  key={c.customerId ?? index}
                  style={{ transition: "0.2s" }}
                  onMouseEnter={(e) =>
                    (e.currentTarget.style.backgroundColor = "#f9fafb")
                  }
                  onMouseLeave={(e) =>
                    (e.currentTarget.style.backgroundColor = "white")
                  }
                >
                  <td style={tdStyle}>{c.firstName} {c.lastName}</td>
                  <td style={tdStyle}>{c.mobileNumber}</td>
                  <td style={tdStyle}>{c.email}</td>
                  <td style={tdStyle}>{c.city}</td>

                  <td style={tdStyle}>
                    <div style={{ display: "flex", gap: "10px" }}>
                      <button onClick={() => handleEdit(c)} style={editBtn}>
                        Edit
                      </button>

                      <button onClick={() => handleDelete(c.customerId!)} style={deleteBtn}>
                        Delete
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default CustomerPage;