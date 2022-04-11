import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderStatus'
})

// This pipe will handle the response for the order's status property in order to visualize
// different values
export class OrderStatusPipe implements PipeTransform {
  transform(value: string): string {
    let result = '';

    switch (value) {
      // When order is created but not paid for
      case "Pending": {
        result = "Order is made, but payment is still pending";
        break;
      }
      case "DeliveryPaid": {
        // When order is created and paid with card
        result = "Card";
        break;
      }
      case "DeliveryCash": {
        // When order is created and customer will pay for it upon arrival
        result = "Cash";
        break;
      }
      case "Completed": {
        // When order is created, paid, recieved and completed
        result = "Order is completed";
        break;
      }
    }

    return result;
  }
}
