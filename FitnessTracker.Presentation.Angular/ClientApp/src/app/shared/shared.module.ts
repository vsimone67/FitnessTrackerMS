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
import { DeleteImageComponent } from '../shared/components/delete-image/delete-image.component'

@NgModule({
  imports: [FormsModule, CommonModule, HttpClientModule],
  declarations: [
    MDL,
    DropDownComponent,
    DropDownComponentMDL,
    SpinnerComponent,
    ToastComponent,
    BaseLayoutComponent,
    DeleteImageComponent
  ],
  exports: [
    MDL,
    DropDownComponent,
    DropDownComponentMDL,
    SpinnerComponent,
    ToastComponent,
    BaseLayoutComponent,
    CommonModule,
    FormsModule,
    DeleteImageComponent
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
