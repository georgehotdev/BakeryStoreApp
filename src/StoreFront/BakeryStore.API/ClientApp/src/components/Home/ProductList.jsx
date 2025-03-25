import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchAllProducts } from "../../state/features/products/productSlice";
import { fetchAllActiveDiscounts } from "../../state/features/discounts/discountSlice";
import ProductItem from "../Home/ProductItem";

export default function ProductList() {
  const dispatch = useDispatch();
  const { products, filter } = useSelector((state) => state.product);
  const { allActiveDiscounts } = useSelector((state) => state.discount);

  useEffect(() => {
    dispatch(fetchAllProducts());
  }, [dispatch]);

  useEffect(() => {
    dispatch(fetchAllActiveDiscounts(filter.date));
  }, [dispatch, filter]);

  return (
    <div>
      <h1>Products</h1>
      <div className="row">
        {products.map((item) => (
          <div className="col-4 pt-4">
            <ProductItem
              key={item.id}
              item={item}
              activeDiscount={allActiveDiscounts[item.id]}
            ></ProductItem>
          </div>
        ))}
      </div>
    </div>
  );
}
