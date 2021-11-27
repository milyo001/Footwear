import { ICartProduct } from "./cartProduct";

export interface IPaymentProduct extends ICartProduct{
  payment: string;
}
