import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'order-status'
})
// This pipe will handle the response for the order's status property in order to visualize
// different values
export class OrderStatusPipe implements PipeTransform {
  transform(value: string): string {
    let result = '';

    switch (value) {
      // When order is created but not paid for
      case "Pending": {
        result = "Pending";
        break;
      }
      case "DeliveryPaid": {
        // When order is created and paid with card
        result = "Paid with card - Delivery process";
        break;
      }
      case "DeliveryCash": {
        // When order is created and customer will pay for it upon arrival
        result = "Cash on delivery - Delivery process";
        break;
      }
      case "Completed": {
        // When order is created and recieved and completed
        result = "Order is recieved";
        break;
      }
    }

    return result;
  }
}
