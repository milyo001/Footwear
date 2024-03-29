import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from '../product/product/product.component';
import { ProductSelectComponent } from '../product/product-select/product-select.component';
import { SharedModule } from './shared.module';

const routes: Routes = [
  { path: '', component: ProductComponent },
  { path: ':id', component: ProductSelectComponent },
];

@NgModule({
  declarations: [
    ProductComponent,
    ProductSelectComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  exports: [
    RouterModule
  ]
})
export class ProductModule { }
