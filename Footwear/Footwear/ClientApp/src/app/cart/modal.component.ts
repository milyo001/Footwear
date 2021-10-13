import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ICartProduct } from '../interfaces/cartProduct';

@Component({
  selector: 'modal-confirm',
  template: `
  <div class="modal-header">
    <h4 class="modal-title" id="modal-title">Confirm product deletion</h4>
    <button type="button" class="close" aria-label="Close button" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
    <p><strong>Are you sure you want to delete <span class="text-primary">{{product.name}}</span> product with total price
    of {{product.price * product.quantity}}$?</strong></p>
    <span class="text-danger">This operation can not be undone.</span>
  </div>
  <div class="modal-footer">
    <button type="button" class="btn btn-outline-secondary" (click)="modal.dismiss('cancel')">No</button>
    <button type="button" ngbAutofocus class="btn btn-danger" (click)="modal.close('confirm')">Yes</button>
  </div>
  `
})

export class ModalComponent {
  constructor(public modal: NgbActiveModal) { }

  @Input() public product: ICartProduct;

  totalPrice: number;


  ngOnInit() {
    
  }
}
