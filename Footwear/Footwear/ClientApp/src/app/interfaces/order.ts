import { ICartProduct } from "./cartProduct";

export interface IOrder {
  products: ICartProduct[];
  payment: string;
  createdOn: string;
  status: string;
}
