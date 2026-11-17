import axios from "axios";

const BASE_URL = "http://localhost:5221/api/customer";

const getAuthConfig = () => {
  const token = localStorage.getItem("token");

  return {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };
};


// ✅ GET
export const getCustomers = async () => {
  const res = await axios.get(BASE_URL, {
    ...getAuthConfig(),
    headers: {
      ...getAuthConfig().headers,
      "Cache-Control": "no-cache",
    },
  });

  return res.data;
};

// ✅ CREATE
export const createCustomer = async (data: any) => {
  const response = await axios.post(
    BASE_URL,
    data,
    getAuthConfig()
  );

  console.log("POST Customer:", response.data);
  return response.data;
};

// ✅ DELETE (THIS IS CORRECT)
export const deleteCustomer = async (id: number) => {
  const response = await axios.delete(
    `${BASE_URL}/${id}`,
    getAuthConfig()
  );

  console.log("DELETE Customer:", response.data);
  return response.data;
};

export const updateCustomer = async (id: number, data: any) => 
{
  const res = await axios.put(`${BASE_URL}/${id}`, data, getAuthConfig());
};