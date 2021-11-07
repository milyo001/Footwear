import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';
import { CdkAccordionModule } from '@angular/cdk/accordion';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product/product.component';
import { AboutComponent } from './about/about.component';
import { ProductSelectComponent } from './product/product-select/product-select.component';
import { CartComponent } from './cart/cart.component';
import { FooterComponent } from './footer/footer.component';

import { UserService } from './services/user.service';
import { AuthInterceptor } from './interceptors/auth.interceptor ';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppRoutingModule } from './modules/routing,module';
import { CookieService } from 'ngx-cookie-service';
import { ModalComponent } from './cart/modal.component';
import { LoadingService } from './services/loading.service';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { MaterialModule } from './modules/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserModule } from './modules/user.module';
import { OrdersComponent } from './orders/orders.component';
import { PlaceOrderComponent } from './orders/place-order/place-order.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductComponent,
    AboutComponent,
    ProductSelectComponent,
    CartComponent,
    FooterComponent,
    ModalComponent,
    OrdersComponent,
    PlaceOrderComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({ progressBar: true, positionClass: 'toast-top-left' }),
    NgbModule,
    CdkAccordionModule,
    NgxPaginationModule,
    FontAwesomeModule,
    UserModule
  ],
  providers:[
    UserService,
    CookieService,
    LoadingService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, //For validating if user is logged in.
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }, //For showing mat-progress-spinner when fetching data.
  ],
  bootstrap: [AppComponent],
  entryComponents: [ModalComponent]
})
export class AppModule { }
