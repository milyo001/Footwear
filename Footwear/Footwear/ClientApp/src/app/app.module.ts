import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, FormControl, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product/product.component';
import { AboutComponent } from './about/about.component';
import { ProductSelectComponent } from './product/product-select/product-select.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { LoaderComponent } from './shared/loader/loader.component';
import { UserService } from './services/user.service';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor ';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductComponent,
    AboutComponent,
    ProductSelectComponent,
    CartComponent,
    LoginComponent,
    RegisterComponent,
    LoaderComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', component: ProductComponent }, //All products
      { path: 'products/:id', component: ProductSelectComponent }, //Product Details
      { path: 'about', component: AboutComponent },
      { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
      { path: 'user/login', component: LoginComponent },
      { path: 'user/register', component: RegisterComponent }
    ]),
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true,
      positionClass: 'toast-top-center'
    }),
    NgbModule,
    NgxPaginationModule,
    
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
