import { __decorate } from "tslib";
import { Component, Input } from '@angular/core';
let ModalComponent = class ModalComponent {
    constructor(modal) {
        this.modal = modal;
    }
    ngOnInit() {
    }
};
__decorate([
    Input()
], ModalComponent.prototype, "product", void 0);
ModalComponent = __decorate([
    Component({
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
], ModalComponent);
export { ModalComponent };
//# sourceMappingURL=modal.component.js.map