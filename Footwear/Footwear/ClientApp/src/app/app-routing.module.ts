import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product.component';
import { ProductSelectComponent } from './product-select/product-select.component';
import { AboutComponent } from './about/about.component';
import { CartComponent } from './cart/cart.component';
import { homedir } from 'os';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'products', component: ProductComponent }, //All products
  { path: 'products/:id', component: ProductSelectComponent }, //Product Details
  { path: 'about', component: AboutComponent },
  { path: 'cart', component: CartComponent },
];

@NgModule({
  declarations: [
    HomeComponent,
    ProductComponent,
    ProductSelectComponent,
    AboutComponent,
    CartComponent
  ],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
