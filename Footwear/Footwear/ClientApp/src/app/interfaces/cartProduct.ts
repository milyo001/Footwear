export interface ICartProduct {
  id: number;
  name: string;
  price: number;
  details: string;
  imageUrl: string;
  gender: string;
  productType: string;
  size: number;
  quantity: number;
  userName: string; //Send the userName to the WebApi to indentify the userId
}
