import { IProduct } from "../Product/product";
import { IOrder } from "./order";


export interface ICompletedOrder extends IOrder {
  products: IProduct[];
}
