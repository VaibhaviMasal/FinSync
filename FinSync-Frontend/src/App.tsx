import { BrowserRouter, Routes, Route} from "react-router-dom";
import Navbar from "./components/Navbar";

import Dashboard from "./pages/Dashboard";
import CustomerPage from "./pages/CustomerPage";
import PolicyPage from "./pages/PolicyPage";

function App()
{
  return (
    <BrowserRouter>
      <Navbar/>

      <Routes>
        <Route path="/" element={<Dashboard/>} />
        <Route path="/customers" element={<CustomerPage/>} />
        <Route path="/policies" element={<PolicyPage/>} />
      </Routes>
      </BrowserRouter>
  );
}

export default App;