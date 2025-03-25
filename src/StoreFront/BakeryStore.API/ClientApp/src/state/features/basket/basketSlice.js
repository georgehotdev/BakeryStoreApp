import { createSlice } from "@reduxjs/toolkit";
import basketServiceGateway from "../../../gateways/basketServiceGateway";

export const basketSlice = createSlice({
  name: "basket",
  initialState: {
    basket: {
      items: [],
      totalPrice: 0
    }
  },
  reducers: {
    addItemToCart: (state, action) => {
      let itemToUpdate = state.basket.items.find(
        (item) =>
          item.productId === (action.payload.id || action.payload.productId)
      );

      if (!itemToUpdate) {
        state.basket.items.push(mapToBasketItem(action.payload));
      } else {
        itemToUpdate.quantity += action.payload.quantity;
      }
    },
    setItemQuantity: (state, action) => {
      let itemToUpdate = state.basket.items.find(
        (item) => item.productId === action.payload.item.productId
      );
      itemToUpdate.quantity = action.payload.newQuantity;
    },
    removeItemFromCart: (state, action) => {
      state.basket.items = state.basket.items.filter(
        (item) => item.productId !== action.payload.productId
      );
    },
    basketLoaded: (state, action) => {
      state.basket = action.payload;
    }
  }
});

const mapToBasketItem = (product) => ({
  quantity: product.quantity,
  price: product.price,
  productId: product.id,
  productName: product.name
});

export const {
  basketLoaded,
  addItemToCart,
  setItemQuantity,
  removeItemFromCart
} = basketSlice.actions;

export const fetchBasket = (date) => async (dispatch) => {
  const response = await basketServiceGateway.get(
    `/api/v1/basket?date=${date}`
  );

  dispatch(basketLoaded(response.data));
};

export const updateBasket =
  (date, updateAction) => async (dispatch, getState) => {
    updateAction();

    const updatedBasket = getState().basket.basket;

    const response = await basketServiceGateway.put(
      `/api/v1/basket?date=${date}`,
      updatedBasket
    );
    console.log("🚀 ~ response.data:", response.data);

    dispatch(basketLoaded(response.data));
  };

export default basketSlice.reducer;
