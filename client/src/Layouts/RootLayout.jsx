import React from "react";

import { Outlet } from "react-router-dom";
import Header from "../Components/Header";
import Footer from "../Components/Footer";

function RootLayout() {
  return (
    <>
      <Header />
      <div className="px-48 w-full h-full">
        {/* Buradaki div'e genel container css verilebilir. */}
        <Outlet />
      </div>
    </>
  );
}

export default RootLayout;
