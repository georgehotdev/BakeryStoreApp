import { createSlice } from "@reduxjs/toolkit";
import catalogServiceGateway from "../../../gateways/catalogServiceGateway";

export const productSlice = createSlice({
  name: "product",
  initialState: {
    filter: {
      date: new Date().toISOString().split("T")[0]
    },
    products: []
  },
  reducers: {
    setFilterDate: (state, action) => {
      state.filter.date = action.payload;
    },
    productsLoaded: (state, action) => {
      state.products = action.payload;
    }
  }
});

export const { productsLoaded } = productSlice.actions;

export const fetchAllProducts = () => async (dispatch) => {
  const response = await catalogServiceGateway.get("/api/v1/catalog");

  dispatch(productsLoaded(response.data));
};

// Action creators are generated for each case reducer function
export const { setFilterDate } = productSlice.actions;

export default productSlice.reducer;
