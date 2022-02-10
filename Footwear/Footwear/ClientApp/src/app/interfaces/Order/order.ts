import { IUserData } from "../User/userData";

export interface IOrder {
  payment: string;
  createdOn: string;
  status: string;
  userData: IUserData;
}
