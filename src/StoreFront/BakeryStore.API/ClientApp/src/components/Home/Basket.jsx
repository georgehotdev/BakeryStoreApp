import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import BasketItem from "./BasketItem";
import {
  setItemQuantity,
  removeItemFromCart,
  updateBasket
} from "../../state/features/basket/basketSlice";

export default function Basket() {
  const { basket } = useSelector((state) => state.basket);
  const dispatch = useDispatch();
  const selectedDate = useSelector((state) => state.product.filter.date);

  const onItemQuantityUpdated = (item, newQuantity) => {
    dispatch(
      updateBasket(selectedDate, () =>
        dispatch(setItemQuantity({ item, newQuantity }))
      )
    );
  };

  const onItemRemovedFromCart = (item) => {
    dispatch(
      updateBasket(selectedDate, () => dispatch(removeItemFromCart(item)))
    );
  };

  return (
    <div className="row justify-content-end">
      <div className="col-10">
        <h4>Your shopping cart</h4>
        {!basket.items.length ? (
          <b>Currently you have no items in the cart :(</b>
        ) : (
          basket.items.map((item) => (
            <div className="mt-4">
              <BasketItem
                key={item.id}
                item={item}
                onQuantityUpdated={onItemQuantityUpdated}
                onItemRemovedFromCart={onItemRemovedFromCart}
              ></BasketItem>
              <hr className="text-secondary" />
            </div>
          ))
        )}
        {basket.items.length ? (
          <div className="mt-4">
            <h6>Total : ${basket.totalPrice}</h6>
          </div>
        ) : (
          <></>
        )}
      </div>
    </div>
  );
}
