import { CurrentMacros } from ".";
export class CalcMacro {
  protected currentMacros: CurrentMacros;
  protected calcMode: number;
  constructor(currentMacros: CurrentMacros) {
    this.currentMacros = currentMacros;
  }
  calcCalories() {
    return this.currentMacros.calories;
  }
  calcWeight() {
    return this.currentMacros.weight;
  }
  calcProtein() {
    return this.calcNumber(
      this.calcCalories(),
      this.currentMacros.protein,
      this.currentMacros.proteinFactor
    );
  }
  caclCarbs() {
    return this.calcNumber(
      this.calcCalories(),
      this.currentMacros.carbs,
      this.currentMacros.carbsFactor
    );
  }
  calcFat() {
    return this.calcNumber(
      this.calcCalories(),
      this.currentMacros.fat,
      this.currentMacros.fatFactor
    );
  }
  calcNumber(mainMacro: number, macroNum: number, factor: number) {
    return Math.round((mainMacro * macroNum) / factor);
  }
}
