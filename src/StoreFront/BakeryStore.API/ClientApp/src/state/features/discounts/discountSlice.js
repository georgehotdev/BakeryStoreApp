import { createSlice } from "@reduxjs/toolkit";
import discountServiceGateway from "../../../gateways/discountServiceGateway";

export const discountSlice = createSlice({
  name: "discount",
  initialState: {
    allActiveDiscounts: {}
  },
  reducers: {
    allActiveDiscountsLoaded: (state, action) => {
      const activeDiscountsDictionary = action.payload.reduce((acc, item) => {
        acc[item.productId] = item;
        return acc;
      }, {});

      state.allActiveDiscounts = activeDiscountsDictionary;
    }
  }
});

export const { allActiveDiscountsLoaded } = discountSlice.actions;

export const fetchAllActiveDiscounts = (date) => async (dispatch) => {
  const response = await discountServiceGateway.get("/api/v1/discount", {
    params: {
      date: date
    }
  });

  dispatch(allActiveDiscountsLoaded(response.data));
};

export default discountSlice.reducer;
