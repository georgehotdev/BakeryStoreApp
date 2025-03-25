import React, { useState } from "react";
import Card from "../../components/common/Card";
import DiscountFlag from "./DiscountFlag";
import NumberInput from "../common/NumberInput";
import Button from "../common/Button";
import {
  addItemToCart,
  updateBasket
} from "../../state/features/basket/basketSlice";
import { useDispatch, useSelector } from "react-redux";

export default function ProductItem({ item, activeDiscount }) {
  const [quantity, setQuantity] = useState(0);
  const dispatch = useDispatch();
  const selectedDate = useSelector((state) => state.product.filter.date);

  const onQuantityChange = (value) => {
    setQuantity(value);
  };

  const addToCart = () => {
    dispatch(
      updateBasket(selectedDate, () =>
        dispatch(addItemToCart({ ...item, quantity }))
      )
    );
  };

  return (
    <Card
      title={item.name}
      description={`$${item.price}`}
      imageUrl={item.imageUrl}
    >
      <DiscountFlag discount={activeDiscount} />
      <div className="row">
        <div className="col">
          <NumberInput onChange={onQuantityChange}></NumberInput>
        </div>
        <div className="col">
          <Button onClick={addToCart}>Add to cart</Button>
        </div>
      </div>
    </Card>
  );
}
