import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { MatButtonModule } from '@angular/material/button'
import { MatInputModule } from '@angular/material/input';

const modules = [
  MatToolbarModule,
  MatProgressSpinnerModule,
  MatButtonModule,
  MatInputModule
];

@NgModule({
  declarations: [
  ],
  imports: modules,
  exports: modules
  
})
export class MaterialModule { }
