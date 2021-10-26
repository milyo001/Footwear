import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { MatButtonModule } from '@angular/material/button'
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';

const modules = [
  MatToolbarModule,
  MatProgressSpinnerModule,
  MatButtonModule,
  MatInputModule,
  MatFormFieldModule
];

@NgModule({
  imports: [CommonModule],
  exports: modules
})
export class MaterialModule { }
