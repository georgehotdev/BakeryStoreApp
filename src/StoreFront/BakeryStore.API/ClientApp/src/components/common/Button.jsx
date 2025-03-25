import React from "react";
import "./Button.css";

export default function Button({ children, onClick, variant }) {
  return (
    <button
      type="button"
      className={`btn btn-${variant || "success"}`}
      onClick={onClick}
    >
      {children}
    </button>
  );
}
