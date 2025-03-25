import React, { useState, useEffect } from "react";

export default function NumberInput({ onChange, value }) {
  const [val, setVal] = useState(value || "");

  useEffect(() => {
    setVal(value || "");
  }, [value]);

  const handleChange = (newVal) => {
    setVal(newVal);
    onChange(newVal);
  };

  return (
    <input
      type="number"
      className="form-control"
      value={val}
      onChange={(e) => handleChange(parseInt(e.target.value))}
    ></input>
  );
}
