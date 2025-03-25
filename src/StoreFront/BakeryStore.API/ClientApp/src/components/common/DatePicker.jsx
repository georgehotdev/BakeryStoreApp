import React from "react";

export default function Filter({ fieldName, label, onChange, value }) {
  return (
    <>
      <label htmlFor={fieldName} className="form-label">
        {label}
      </label>
      <input
        value={value}
        type="date"
        className="form-control"
        onChange={(e) => onChange(e.target.value)}
      />
    </>
  );
}
