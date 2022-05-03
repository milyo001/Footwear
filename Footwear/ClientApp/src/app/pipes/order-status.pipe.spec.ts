import { OrderStatusPipe } from "./order-status.pipe";

describe('BoldPipe', () => {
    const pipe = new OrderStatusPipe();

    it('transforms payment status "Pending" to "Payment pending"', () => {
      expect(pipe.transform('Pending')).toBe('Payment pending');
    });

    it('transforms payment status "DeliveryPaid" to "Card"', () => {
      expect(pipe.transform('DeliveryPaid')).toBe('Card');
    });

    it('transforms payment status "DeliveryCash" to "Cash"', () => {
      expect(pipe.transform('DeliveryCash')).toBe('Cash');
    });

    it('transforms payment status "Completed" to "Completed"', () => {
      expect(pipe.transform('Completed')).toBe('Completed');
    });
});
