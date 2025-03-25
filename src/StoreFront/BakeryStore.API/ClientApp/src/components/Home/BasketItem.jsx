import React, { useEffect } from "react";
import NumberInput from "../common/NumberInput";
import Button from "../common/Button";
import "./BasketItem.css";

export default function BasketItem({
  item,
  onQuantityUpdated,
  onItemRemovedFromCart
}) {
  return (
    <div className="row">
      <div className="col-4">
        {item.productName}
        <br />
        {item.discountAmount && (
          <b className="discount-text">{item.discountDescription}</b>
        )}
      </div>
      <div className="col-3">
        <NumberInput
          value={item.quantity}
          onChange={(quantity) => onQuantityUpdated(item, quantity)}
        ></NumberInput>
      </div>
      <div className="col-2">
        {item.discountAmount && (
          <span class="badge rounded-pill bg-danger">
            $-{item.discountAmount}
          </span>
        )}
      </div>
      <div className="col-2">
        ${item.quantity * item.price - (item.discountAmount || 0)}
      </div>
      <div className="col-1">
        <Button variant={"danger"} onClick={() => onItemRemovedFromCart(item)}>
          x
        </Button>
      </div>
    </div>
  );
}
