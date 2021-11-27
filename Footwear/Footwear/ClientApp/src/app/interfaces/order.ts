import { ICartProduct } from "./cartProduct";
import { IUserData } from "./userData";

export interface IOrder {
  products: ICartProduct[];
  payment: string;
  createdOn: string;
  status: string;
  userData: IUserData;
}
