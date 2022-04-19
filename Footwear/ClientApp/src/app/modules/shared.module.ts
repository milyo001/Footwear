import { CdkAccordionModule } from "@angular/cdk/accordion";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { UserDataComponent } from "../shared/userData/user-data.component";
import { MaterialModule } from "./material.module";

const modules = [
  CommonModule,
  MaterialModule,
  FormsModule,
  ReactiveFormsModule,
  CdkAccordionModule,
  FontAwesomeModule
];

const components = [ UserDataComponent ];

@NgModule({
  declarations: [ components ],
  imports: [CommonModule],
  exports: [ modules, components ]
})
export class SharedModule { }
