import React, { useEffect } from "react";
import Filter from "../components/Home/Filter";
import ProductList from "../components/Home/ProductList";
import Basket from "../components/Home/Basket";
import { useDispatch, useSelector } from "react-redux";
import { fetchBasket } from "../state/features/basket/basketSlice";

export default function Home() {
  const dispatch = useDispatch();
  const selectedDate = useSelector((state) => state.product.filter.date);

  useEffect(() => {
    dispatch(fetchBasket(selectedDate));
  }, [dispatch]);

  return (
    <div className="container">
      <div className="row">
        <div className="col-4">
          <div className="mb-3">
            <Filter></Filter>
          </div>
        </div>
      </div>
      <div className="row">
        <div className="col-8">
          <ProductList />
        </div>
        <div className="col-4">
          <Basket></Basket>
        </div>
      </div>
    </div>
  );
}
