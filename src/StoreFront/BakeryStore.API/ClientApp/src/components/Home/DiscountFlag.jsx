import React from "react";
import "./DiscountFlag.css";

export default function DiscountFlag({ discount }) {
  return (
    <>
      {discount && <div className="discount-flag"> {discount.description}</div>}
    </>
  );
}
