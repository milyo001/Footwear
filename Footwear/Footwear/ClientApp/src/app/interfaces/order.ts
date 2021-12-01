import { IUserData } from "./userData";

export interface IOrder {
  payment: string;
  createdOn: string;
  status: string;
  userData: IUserData;
}
