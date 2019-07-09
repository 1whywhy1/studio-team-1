/// <summary>
/// The type of item.
/// </summary>
public enum ItemType
{
	Unassigned,
	Food,
	Meds,
	Parts,
	Rags
};

public enum TradeType
{
	Unassigned,
	MedsForFood,
	MedsForParts,
	MedsForRags,
	FoodForParts,
	FoodForRags,
	RagsForParts,
	FoodForMeds,
	PartsForMeds,
	RagsForMeds,
	PartsForFood,
	RagsForFood,
	PartsForRags
};
