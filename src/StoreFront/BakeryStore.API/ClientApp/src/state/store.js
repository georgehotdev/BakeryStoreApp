import { configureStore } from "@reduxjs/toolkit";
import productReducer from "./features/products/productSlice";
import discountReducer from "./features/discounts/discountSlice";
import basketReducer from "./features/basket/basketSlice";

export default configureStore({
  reducer: {
    product: productReducer,
    discount: discountReducer,
    basket: basketReducer
  }
});
