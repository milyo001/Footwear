import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from '../product/product/product.component';
import { ProductSelectComponent } from '../product/product-select/product-select.component';
import { NgxPaginationModule } from 'ngx-pagination';

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
    NgxPaginationModule
  ],
  exports: [
    RouterModule
  ]
})
export class ProductModule { }
