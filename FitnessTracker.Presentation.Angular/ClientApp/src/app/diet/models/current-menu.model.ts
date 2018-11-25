export class CurrentMenu {
  Id: string;
  ItemID: number;
  Serving: number;
  ServingSize: string;
  Item: string;

  constructor(id: string, itemId: number, serving: number, servingSize: string, item: string) {
    this.Id = id;
    this.ItemID = itemId;
    this.Serving = serving;
    this.ServingSize = servingSize;
    this.Item = item;
  }
}
