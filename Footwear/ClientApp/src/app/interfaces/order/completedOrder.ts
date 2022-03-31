import { ICartProduct } from "../cart/cartProduct";
import { IOrder } from "./order";


export interface ICompletedOrder extends IOrder {
  products: ICartProduct[];
}
