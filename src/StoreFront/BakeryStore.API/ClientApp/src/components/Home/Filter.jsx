import React from "react";
import DatePicker from "../common/DatePicker";
import { useDispatch, useSelector } from "react-redux";
import { setFilterDate } from "../../state/features/products/productSlice";

export default function Filter() {
  const dispatch = useDispatch();
  const selectedDate = useSelector((state) => state.product.filter.date);
  const handleDateChange = (date) => {
    dispatch(setFilterDate(date));
  };

  return (
    <DatePicker
      fieldName={"date"}
      label={"Date"}
      onChange={handleDateChange}
      value={selectedDate}
    />
  );
}
