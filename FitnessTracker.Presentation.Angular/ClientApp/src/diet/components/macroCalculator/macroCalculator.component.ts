import { Component, OnInit, ElementRef, Inject } from '@angular/core';
import  * as Redux  from 'redux';

import { EventService } from '../../../shared/services';
import { DietService } from '../../services/diet.service';
import { BaseComponent } from '../../../shared/components';
import { DietPlannerActions, DIETPLANNER_ACTIONS } from '../../../shared/actions';
import { AppStore, AppState } from '../../../app';
import { MeatabolicInfo, MeatabolicInfoConstants } from '../../models';

@Component({
    moduleId: module.id,
    selector: 'macrocalc',
    templateUrl: 'macroCalculator.component.html'
})
export class MacroCalculatorComponent extends BaseComponent implements OnInit {
    dialog: any;
    metabolicInfo: Array<MeatabolicInfo>;
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

    constructor(private _el: ElementRef, private _dietService: DietService, public _eventService: EventService,
        private _dietPlannerActions: DietPlannerActions, @Inject(AppStore) private _store: Redux.Store<AppState>) {
        super(_eventService);
        this.metabolicInfo = new Array<MeatabolicInfo>();

        _store.subscribe(() => this.updateState());
    }

    ngOnInit() {
        this._dietService.getMetabolicInfo((metabolicInfo: Array<MeatabolicInfo>) => {
            this._dietPlannerActions.updateStore<Array<MeatabolicInfo>>(metabolicInfo, DIETPLANNER_ACTIONS.GET_METABOLICINFO);
        });
    }
    showDialog() {
        this.dialog = this._el.nativeElement.querySelector('dialog');

        if (!this.dialog.showModal) {
            this.dialog.dialogPolyfill.registerDialog(this.dialog);
        }

        this.dialog.showModal();
    }
    reCalcMacro(event: any) {

        this.calcMacros();
    }
    onSave() {
        let weight: MeatabolicInfo = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.weight);
        weight.cut = this.weight;
        weight.maintain = this.weight;
        weight.gain = this.weight;

        this._dietService.saveMetabolicInfo(weight, () => {
            this.showMessage('Weight Saved');
        });

        let dcr = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.dcr);
        dcr.cut = this.dcr;
        dcr.maintain = this.dcr;
        dcr.gain = this.dcr;

        this._dietService.saveMetabolicInfo(dcr, () => {
            this.showMessage('DCR Saved');
        });

        let caloriePer = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.caldefper);
        caloriePer.cut = this.calorieDeficitPerCut / 100;
        caloriePer.maintain = this.calorieDeficitPerMaintain / 100;
        caloriePer.gain = this.calorieDeficitPerGain / 100;

        this._dietService.saveMetabolicInfo(caloriePer, () => {
            this.showMessage('Calories % Saved');
        });

        let calorieMax = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.calories);
        calorieMax.cut = this.newCalorieCut
        calorieMax.maintain = this.newCalorieMaintain
        calorieMax.gain = this.newCalorieGain

        this._dietService.saveMetabolicInfo(calorieMax, () => {
            this.showMessage('Calories Saved'); 
        });

        let fatPer = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.fatper);
        fatPer.cut = this.fatPerCut / 100;
        fatPer.maintain = this.fatPerMaintain / 100;
        fatPer.gain = this.fatPerGain / 100;

        this._dietService.saveMetabolicInfo(fatPer, () => {
            this.showMessage('Fat Saved');
        });

        let proteinPer = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.proteinper);
        proteinPer.cut = this.proteinPerCut / 100;
        proteinPer.maintain = this.proteinPerMaintain / 100;
        proteinPer.gain = this.proteinPerGain / 100;

        this._dietService.saveMetabolicInfo(proteinPer, () => {
            this.showMessage('Protein Saved');
        });

        let carbsPer = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.carbsper);
        carbsPer.cut = this.carbsPerCut / 100;
        carbsPer.maintain = this.carbsPerMaintain / 100;
        carbsPer.gain = this.carbsPerGain / 100;

        this._dietService.saveMetabolicInfo(carbsPer, () => {
            this.showMessage('Carbs Saved');
        });
        
        this.onClose();
    }
    onClose() {
        this.dialog.close();
    }
    getMacroInputs() {
        // these are the variables that can be changed on the screen
        this.weight = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.weight).cut;
        this.dcr = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.dcr).cut;
        this.calorieDeficitPerCut = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.caldefper).cut * 100;
        this.calorieDeficitPerMaintain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.caldefper).maintain * 100;
        this.calorieDeficitPerGain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.caldefper).gain * 100;

        this.fatPerCut = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.fatper).cut * 100;
        this.fatPerMaintain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.fatper).maintain * 100;
        this.fatPerGain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.fatper).gain * 100;

        this.proteinPerCut = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.proteinper).cut * 100;
        this.proteinPerMaintain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.proteinper).maintain * 100;
        this.proteinPerGain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.proteinper).gain * 100;

        this.carbsPerCut = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.carbsper).cut * 100;
        this.carbsPerMaintain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.carbsper).maintain * 100;
        this.carbsPerGain = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.carbsper).gain * 100;
    }
    calcMacros() {

        this.activityLevel = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.actlvl).cut;
        this.bmr = this.metabolicInfo.find(exp => exp.macro === MeatabolicInfoConstants.bmr).cut;


        this.calorieDeficitCut = Math.round((this.calorieDeficitPerCut / 100) * this.dcr);
        this.calorieDeficitMaintain = Math.round((this.calorieDeficitPerMaintain / 100) * this.dcr);
        this.calorieDeficitGain = Math.round((this.calorieDeficitPerGain / 100) * this.dcr);
        this.newCalorieCut = Number(this.dcr) - this.calorieDeficitCut;
        this.newCalorieMaintain = Number(this.dcr) - this.calorieDeficitMaintain;
        this.newCalorieGain = Number(this.dcr) + this.calorieDeficitGain;

        this.fatCaloriesCut = Math.round((this.fatPerCut / 100) * this.newCalorieCut);
        this.fatGramsCut = Math.round(this.fatCaloriesCut / 9);
        this.fatCaloriesMaintain = Math.round((this.fatPerMaintain / 100) * this.newCalorieMaintain);
        this.fatGramsMaintain = Math.round(this.fatCaloriesMaintain / 9);
        this.fatCaloriesGain = Math.round((this.fatPerGain / 100) * this.newCalorieGain);
        this.fatGramsGain = Math.round(this.fatCaloriesGain / 9);

        this.proteinCaloriesCut = Math.round((this.proteinPerCut / 100) * this.newCalorieCut);
        this.proteinGramsCut = Math.round(this.proteinCaloriesCut / 4);
        this.proteinCaloriesMaintain = Math.round((this.proteinPerMaintain / 100) * this.newCalorieMaintain);
        this.proteinGramsMaintain = Math.round(this.proteinCaloriesMaintain / 4);
        this.proteinCaloriesGain = Math.round((this.proteinPerGain / 100) * this.newCalorieGain);
        this.proteinGramsGain = Math.round(this.proteinCaloriesGain / 4);

        this.carbsCaloriesCut = Math.round((this.carbsPerCut / 100) * this.newCalorieCut);
        this.carbsGramsCut = Math.round(this.carbsCaloriesCut / 4);
        this.carbsCaloriesMaintain = Math.round((this.carbsPerMaintain / 100) * this.newCalorieMaintain);
        this.carbsGramsMaintain = Math.round(this.carbsCaloriesMaintain / 4);
        this.carbsCaloriesGain = Math.round((this.carbsPerGain / 100) * this.newCalorieGain);
        this.carbsGramsGain = Math.round(this.carbsCaloriesGain / 4);


    }

    updateState() {
        let state = this._store.getState();

        this.metabolicInfo = state.metabolicInfo.metabolicInfo;
        this.getMacroInputs();
        this.calcMacros();

    }
}
