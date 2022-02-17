import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';
import { ProductComponent } from '../product/product/product.component';
import { ProductSelectComponent } from '../product/product-select/product-select.component';

const routes: Routes = [
  { path: '', component: ProductComponent },
  { path: ':id', component: ProductSelectComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class ProductModule { }
