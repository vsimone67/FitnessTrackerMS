import { NgModule, ModuleWithProviders } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import {
  DropDownComponent,
  DropDownComponentMDL,
  SpinnerComponent,
  ToastComponent
} from "../shared/components";
import { MDL } from "../shared/directives";
import { EventService } from "../shared/services";
import { BaseLayoutComponent } from "../layout";
import { AppsettingsService } from "./services/app-settings.service";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  imports: [FormsModule, CommonModule, HttpClientModule],
  declarations: [
    MDL,
    DropDownComponent,
    DropDownComponentMDL,
    SpinnerComponent,
    ToastComponent,
    BaseLayoutComponent
  ],
  exports: [
    MDL,
    DropDownComponent,
    DropDownComponentMDL,
    SpinnerComponent,
    ToastComponent,
    BaseLayoutComponent,
    CommonModule,
    FormsModule
  ],
  providers: [EventService, AppsettingsService]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: []
    };
  }
}
