import { Component, OnInit, ElementRef, Inject } from "@angular/core";
import { EventService } from "../../../shared/services";
import { BaseComponent } from "../../../shared/components";
import { MeatabolicInfo, MeatabolicInfoConstants } from "../../models";
import { Store, Select } from "@ngxs/store";
import { Observable } from "rxjs/observable";
import { GetMetabolicInfo, SaveMetabolicInfo } from "../../actions/metabolic-info.actions"
import { MetabolicInfoState } from '../../state/metabolic-info.state'

@Component({
  selector: "macrocalc",
  templateUrl: "macro-calculator.component.html"
})
export class MacroCalculatorComponent extends BaseComponent implements OnInit {
  dialog: any;
  private metabolicInfo: Array<MeatabolicInfo>;
  private weight: number;
  private activityLevel: number;
  private bmr: number;
  private dcr: number;

  private calorieDeficitPerCut: number;
  private calorieDeficitPerMaintain: number;
  private calorieDeficitPerGain: number;

  private calorieDeficitCut: number;
  private calorieDeficitMaintain: number;
  private calorieDeficitGain: number;

  private newCalorieCut: number;
  private newCalorieMaintain: number;
  private newCalorieGain: number;

  private fatPerCut: number;
  private fatPerMaintain: number;
  private fatPerGain: number;

  private fatCaloriesCut: number;
  private fatCaloriesMaintain: number;
  private fatCaloriesGain: number;

  private fatGramsCut: number;
  private fatGramsMaintain: number;
  private fatGramsGain: number;

  private proteinPerCut: number;
  private proteinPerMaintain: number;
  private proteinPerGain: number;

  private proteinCaloriesCut: number;
  private proteinCaloriesMaintain: number;
  private proteinCaloriesGain: number;

  private proteinGramsCut: number;
  private proteinGramsMaintain: number;
  private proteinGramsGain: number;

  private carbsPerCut: number;
  private carbsPerMaintain: number;
  private carbsPerGain: number;

  private carbsCaloriesCut: number;
  private carbsCaloriesMaintain: number;
  private carbsCaloriesGain: number;

  private carbsGramsCut: number;
  private carbsGramsMaintain: number;
  private carbsGramsGain: number;

  @Select(MetabolicInfoState.metabolicInfoList) metabolidInfoList$: Observable<Array<MeatabolicInfo>>;

  constructor(
    private _el: ElementRef,
    private _store: Store,
    public _eventService: EventService
  ) {
    super(_eventService);
    this.metabolicInfo = new Array<MeatabolicInfo>();
  }

  ngOnInit() {
    this._store.dispatch(new GetMetabolicInfo()).subscribe( () =>  this.metabolidInfoList$.subscribe(metabolicInfoList => this.metabolicInfo = metabolicInfoList ));
  }
  showDialog() {
    this.dialog = this._el.nativeElement.querySelector("dialog");

    if (!this.dialog.showModal) {
      this.dialog.dialogPolyfill.registerDialog(this.dialog);
    }

    this.dialog.showModal();
  }
  reCalcMacro(event: any) {
    this.calcMacros();
  }
  onSave() {
    let weight: MeatabolicInfo = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.weight
    );
    weight.cut = this.weight;
    weight.maintain = this.weight;
    weight.gain = this.weight;

    this._store.dispatch(new SaveMetabolicInfo(weight)).subscribe( () =>  this.showMessage("Weight Saved")); 

    let dcr = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.dcr
    );
    dcr.cut = this.dcr;
    dcr.maintain = this.dcr;
    dcr.gain = this.dcr;

    this._store.dispatch(new SaveMetabolicInfo(dcr)).subscribe( () =>  this.showMessage("DCR Saved")); 

    let caloriePer = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.caldefper
    );
    caloriePer.cut = this.calorieDeficitPerCut / 100;
    caloriePer.maintain = this.calorieDeficitPerMaintain / 100;
    caloriePer.gain = this.calorieDeficitPerGain / 100;

    this._store.dispatch(new SaveMetabolicInfo(caloriePer)).subscribe( () =>  this.showMessage("Calorie % Saved")); 

    let calorieMax = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.calories
    );
    calorieMax.cut = this.newCalorieCut;
    calorieMax.maintain = this.newCalorieMaintain;
    calorieMax.gain = this.newCalorieGain;

    this._store.dispatch(new SaveMetabolicInfo(calorieMax)).subscribe( () =>  this.showMessage("Calorie Max Saved")); 

    let fatPer = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.fatper
    );
    fatPer.cut = this.fatPerCut / 100;
    fatPer.maintain = this.fatPerMaintain / 100;
    fatPer.gain = this.fatPerGain / 100;

    this._store.dispatch(new SaveMetabolicInfo(fatPer)).subscribe( () =>  this.showMessage("Fat Saved")); 

    let proteinPer = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.proteinper
    );
    proteinPer.cut = this.proteinPerCut / 100;
    proteinPer.maintain = this.proteinPerMaintain / 100;
    proteinPer.gain = this.proteinPerGain / 100;

    this._store.dispatch(new SaveMetabolicInfo(proteinPer)).subscribe( () =>  this.showMessage("Protein Saved")); 

    let carbsPer = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.carbsper
    );
    carbsPer.cut = this.carbsPerCut / 100;
    carbsPer.maintain = this.carbsPerMaintain / 100;
    carbsPer.gain = this.carbsPerGain / 100;

    this._store.dispatch(new SaveMetabolicInfo(carbsPer)).subscribe( () =>  this.showMessage("Carbs Saved")); 

    this.onClose();
  }
  onClose() {
    this.dialog.close();
  }
  getMacroInputs() {
    // these are the variables that can be changed on the screen
    this.weight = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.weight
    ).cut;
    this.dcr = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.dcr
    ).cut;
    this.calorieDeficitPerCut =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.caldefper
      ).cut * 100;
    this.calorieDeficitPerMaintain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.caldefper
      ).maintain * 100;
    this.calorieDeficitPerGain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.caldefper
      ).gain * 100;

    this.fatPerCut =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.fatper
      ).cut * 100;
    this.fatPerMaintain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.fatper
      ).maintain * 100;
    this.fatPerGain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.fatper
      ).gain * 100;

    this.proteinPerCut =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.proteinper
      ).cut * 100;
    this.proteinPerMaintain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.proteinper
      ).maintain * 100;
    this.proteinPerGain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.proteinper
      ).gain * 100;

    this.carbsPerCut =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.carbsper
      ).cut * 100;
    this.carbsPerMaintain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.carbsper
      ).maintain * 100;
    this.carbsPerGain =
      this.metabolicInfo.find(
        exp => exp.macro === MeatabolicInfoConstants.carbsper
      ).gain * 100;
  }
  calcMacros() {
    this.activityLevel = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.actlvl
    ).cut;
    this.bmr = this.metabolicInfo.find(
      exp => exp.macro === MeatabolicInfoConstants.bmr
    ).cut;

    this.calorieDeficitCut = Math.round(
      (this.calorieDeficitPerCut / 100) * this.dcr
    );
    this.calorieDeficitMaintain = Math.round(
      (this.calorieDeficitPerMaintain / 100) * this.dcr
    );
    this.calorieDeficitGain = Math.round(
      (this.calorieDeficitPerGain / 100) * this.dcr
    );
    this.newCalorieCut = Number(this.dcr) - this.calorieDeficitCut;
    this.newCalorieMaintain = Number(this.dcr) - this.calorieDeficitMaintain;
    this.newCalorieGain = Number(this.dcr) + this.calorieDeficitGain;

    this.fatCaloriesCut = Math.round(
      (this.fatPerCut / 100) * this.newCalorieCut
    );
    this.fatGramsCut = Math.round(this.fatCaloriesCut / 9);
    this.fatCaloriesMaintain = Math.round(
      (this.fatPerMaintain / 100) * this.newCalorieMaintain
    );
    this.fatGramsMaintain = Math.round(this.fatCaloriesMaintain / 9);
    this.fatCaloriesGain = Math.round(
      (this.fatPerGain / 100) * this.newCalorieGain
    );
    this.fatGramsGain = Math.round(this.fatCaloriesGain / 9);

    this.proteinCaloriesCut = Math.round(
      (this.proteinPerCut / 100) * this.newCalorieCut
    );
    this.proteinGramsCut = Math.round(this.proteinCaloriesCut / 4);
    this.proteinCaloriesMaintain = Math.round(
      (this.proteinPerMaintain / 100) * this.newCalorieMaintain
    );
    this.proteinGramsMaintain = Math.round(this.proteinCaloriesMaintain / 4);
    this.proteinCaloriesGain = Math.round(
      (this.proteinPerGain / 100) * this.newCalorieGain
    );
    this.proteinGramsGain = Math.round(this.proteinCaloriesGain / 4);

    this.carbsCaloriesCut = Math.round(
      (this.carbsPerCut / 100) * this.newCalorieCut
    );
    this.carbsGramsCut = Math.round(this.carbsCaloriesCut / 4);
    this.carbsCaloriesMaintain = Math.round(
      (this.carbsPerMaintain / 100) * this.newCalorieMaintain
    );
    this.carbsGramsMaintain = Math.round(this.carbsCaloriesMaintain / 4);
    this.carbsCaloriesGain = Math.round(
      (this.carbsPerGain / 100) * this.newCalorieGain
    );
    this.carbsGramsGain = Math.round(this.carbsCaloriesGain / 4);
  }
}
