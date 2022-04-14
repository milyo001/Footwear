import { IUserData } from "../User/userData";

export interface IOrder {
  orderId: string;
  payment: string;
  createdOn: string;
  status: string;
  userData: IUserData;
}
