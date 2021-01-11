using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InnTech.SqlDataGenerator
{
    public class GeneratorSettings
    {
        public string ConnectionString { get; set; }

        public Dictionary<string, int> CreateEntities { get; set; } = new Dictionary<string, int>
        {
            { "IntVehicleFamily",5 },
            { "IntDefectExternalManifestation", 5 }
        };
        /// <summary>
        /// Default values, where <c>CustomValues.Key</c> is column name,
        /// but <c>CustomValues.Value</c> is column value (use quotes or special symbols if needed).
        /// </summary>
        public Dictionary<string, string> CustomValues { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// List of column names to set current date in UTC.
        /// </summary>
        public List<string> SetDateTimeNow { get; set; } = new List<string> { "CreatedOn", "ModifiedOn" };

        #region Properties to generate random typed values.

        public int NVarCharLength { get; set; } = 7;

        public DateTime StartDateTime2 { get; set; } = DateTime.UnixEpoch;
        public DateTime FinishDateTime2 { get; set; } = DateTime.UtcNow;

        public int MaxInt { get; set; } = 100;
        public int MinInt { get; set; } = 0;

        public int VarBinarySize { get; set; } = 10;
        public int BinarySize { get; set; } = 10;

        public int MaxBigInt { get; set; } = 100;
        public int MinBigInt { get; set; } = 0;

        public DateTime StartDate { get; set; } = DateTime.UnixEpoch.Date;
        public DateTime FinishDate { get; set; } = DateTime.UtcNow.Date;

        [JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan MinTime { get; set; } = new TimeSpan(0, 0, 0);

        [JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan MaxTime { get; set; } = new TimeSpan(23, 59, 59);

        public double MinFloat { get; set; } = 10;
        public double MaxFloat { get; set; } = 1000;

        public float MinReal { get; set; } = 10;
        public float MaxReal { get; set; } = 1000;

        public DateTime StartDateTime { get; set; } = DateTime.UnixEpoch;
        public DateTime FinishDateTime { get; set; } = DateTime.UtcNow;

        public decimal MinDecimal { get; set; } = 1000;
        public decimal MaxDecimal { get; set; } = 100000;

        public int TextLength { get; set; } = 25;

        public DateTime StartSmallDateTime { get; set; } = DateTime.UnixEpoch;
        public DateTime FinishSmallDateTime { get; set; } = DateTime.UtcNow;

        public int CharLength { get; set; } = 3;

        public int NTextLength { get; set; } = 8;

        public int VarCharLength { get; set; } = 6;

        public decimal MinMoney { get; set; } = 1000;
        public decimal MaxMoney { get; set; } = 10000000;

        public int NCharLength { get; set; } = 6;


        public byte MinTinyInt { get; set; } = 0;
        public byte MaxTinyInt { get; set; } = byte.MaxValue;

        public int TimestampSize { get; set; } = 8;

        public short MinSmallInt { get; set; } = 0;
        public short MaxSmallInt { get; set; } = short.MaxValue;

        public decimal MinSmallMoney { get; set; } = 100;
        public decimal MaxSmallMoney { get; set; } = 100000;

        public DateTimeOffset StartDateTimeOffset { get; set; } = DateTimeOffset.UnixEpoch;
        public DateTimeOffset FinishDateTimeOffset { get; set; } = DateTimeOffset.UtcNow;

        public List<string> Vocabulary { get; set; } = new List<string>()
        {
            "Lustrous", "Vigilant", "Slack", "Fracas", "Tractable", "Pertain", "Ferment", "Recant", "Apropos", "Incursion", "Insouciant", "Gist", "Cravat", "Debacle", "Oblivious", "Fledged", "Recondite", "Clamor", "Antidote", "Apprise", "Plaque", "Equilibrium", "Sash", "Consummate", "Transgress", "Quail", "Indomitable", "Soporific", "Dereliction", "Sanction", "Intersperse", "Exuberance", "Indefatigability", "Severance", "Saturnine", "Anguish", "Partisan", "Discrete", "Lull", "Extinguish", "Coy", "Obstreperous", "Sermon", "Susceptibility", "Restive", "Legacy", "Substantiation", "Blatant", "Lien", "Remonstrate", "Calumny", "Sinuous", "Barren", "Salutary", "Quirk", "Derivative", "Virago", "Intransigent", "Gust", "Incongruous", "Prevalent", "Quiescence", "Irresolute", "Purvey", "Dainty", "Immaculate", "Palpability", "Impromptu", "Serration", "Squat", "Hoodwink", "Pellucid", "Reactionary", "Clinch", "Travesty", "Forgery", "Gregarious", "Curriculum", "Rift", "Heresy", "Solvent", "Aplomb", "Torque", "Visceral", "Fragrant", "Inundate", "Chisel", "Ire", "Commodious", "Stray", "Sting", "Ascend", "Surfeit", "Adamant", "Aloof", "Empirical", "Splenetic", "Cower", "Facetious", "Impending", "Maverick", "Encapsulate", "Interim", "Complaisance", "Qualm", "Stygian", "Suffocate", "Apartheid", "Discern", "Wend", "Imperviousness", "Turgid", "Insensible", "Indelible", "Recuperate", "Excoriation", "Forge", "Rarefy", "Irascible", "Indigenous", "Suppliant", "Tautology", "Indolence", "Prone", "Imperturbable", "Detumescence", "Perjury", "Impugned", "Pivotal", "Obtrusive", "Stanch", "Germane", "Profligacy", "Boorish", "Inane", "Snare", "Cabal", "Imprecation", "Clot", "Froward", "Snub", "Articulate", "Ulterior", "Loquacious", "Deprave", "Revere", "Console", "Disinter", "Alloy", "Aver", "Ebullience", "Atonement", "Prosaic", "Cajole", "Patron", "Disproof", "Somatic", "Pariah", "Desuetude", "Dexterity", "Ostentation", "Garble", "Probity", "Augury", "Quandary", "Bilge", "Hermetic", "Penury", "Glut", "Disingenuous", "Repine", "Strut", "Topple", "Premature", "Suffice", "Roll", "Apotheosis", "Candid", "Assail", "Finical", "Boisterous", "Shrill", "Tyro", "Adorn", "Fecund", "Umbrage", "Pry", "Proscribe", "Rebuff", "Veer", "Welter", "Plunge", "Ingenuous", "Warrant", "Idiosyncrasy", "Multifarious", "Conjoin", "Ineffable", "Sere", "Pious", "Miser", "Attune", "Slur", "Torment", "Stride", "Submerge", "Abeyance", "Pique", "Nugatory", "Swerve", "Penchant", "Recitals", "Obdurate", "Discourse", "Trepidation", "Harbinger", "Correlate", "Presage", "Incise", "Gorge", "Indigence", "Malapropism", "Ominous", "Permeate", "Vigilance", "Petrous", "Effete", "Seminal", "Mesmerize", "Pluck", "Supersede", "Vivacious", "Feud", "Profuse", "Clientele", "Platitude", "Pyre", "Ossify", "Resuscitation", "Shard", "Trenchant", "Holster", "Delineate", "Turbid", "Limp", "Figurehead", "Frenetic", "Eschew", "Impair", "Avid", "Chary", "Gainsay", "Cordial", "Irrevocable", "Entice", "Abraded", "Burgeon", "Felon", "Viscous", "Alacrity", "Exploit", "Precursory", "Pugnacious", "Benefactor", "Dormant", "Enzyme", "Scorch", "Toady", "Calipers", "Ensign", "Veracity", "Arduous", "Turbulence", "Incite", "Extirpate", "Discountenance", "Grovel", "Disconcert", "Opaqueness", "Countenance", "Embellish", "Paradigm", "Mendacity", "Countervail", "Apt", "Bogus", "Renovate", "Matriculation", "Grave", "Stint", "Cant", "Putrefaction", "Peripatetic", "Derision", "Provident", "Guile", "Sever", "Supercilious", "Fulmination", "Belabor", "Vestige", "Imbroglio", "Exscind", "Predilection", "Repulsive", "Thwart", "Equivocate", "Voluble", "Exhaustive", "Congeal", "Chastisement", "Mettle", "Contemn", "Brittle", "Asperity", "Curmudgeon", "Covert", "Coeval", "Consternation", "Ramify", "Fleet", "Deposition", "Ubiquitous", "Aversion", "Endemic", "Nonchalant", "Diaphanous", "Efficacy", "Extrovert", "Equable", "Invective", "Redeem", "Valorous", "Harangue", "Hallow", "Peccadillo", "Argot", "Quaff", "Covetous", "Censure", "Lumber", "Ail", "Vindictive", "Desultory", "Finicky", "Detraction", "Salacious", "Undulate", "Fallacious", "Mischievous", "Felicitous", "Animosity", "Blandishment", "Glean", "Abrogate", "Dearth", "Dismal", "Drone", "Macabre", "Knit", "Florid", "Convoluted", "Droll", "Incumbents", "Petrified", "Labyrinthine", "Inveigh", "Muffler", "Presumption", "Inept", "Insurrection", "Subsume", "Macerate", "Beatify", "Tamp", "Garrulity", "Garment", "Petrify", "Writ", "Perfidy", "Levity", "Connotation", "Embezzle", "Impetuous", "Expiation", "Ambidextrous", "Condense", "Appease", "Wag", "Tangential", "Imminent", "Prudish", "Soggy", "Doggerel", "Aseptic", "Infuse", "Disparate", "Earthenware", "Noisome", "Arrogance", "Valent", "Inclined", "Baleful", "Euthanasia", "Warmonger", "Astringent", "Fidelity", "Deplete", "Engrave", "Plaintive", "Overhaul", "Recast", "Extricable", "Abet", "Feckless", "Recalcitrant", "Inimitable", "Ostensible", "Renowned", "Fixate", "Odor", "Quiescent", "Equipoise", "Sullied", "Hoi", "Colander", "Acclaimed", "Liberality", "Cogitate", "Evoke", "Pristine", "Malleable", "Disallow", "Vitiate", "Centurion", "Sublime", "Valiant", "Raconteur", "Vex", "Constrict", "Shun", "Stigma", "Vilify", "Sophistry", "Crease", "Commuter", "Talon", "Abstruse", "Deviance", "Elaborate", "Secular", "Auspicious", "Deprecate", "Esoteric", "Vagary", "Riddle", "Dud", "Expostulate", "Mercurial", "Simper", "Forage", "Eradicate", "Wean", "Duplicity", "Ferret", "Plea", "Impervious", "Forfeit", "Subdue", "Servile", "Puissance", "Dolt", "Impediment", "Loll", "Tawdry", "Judicious", "Volubility", "Taunt", "Drawl", "Abdication", "Credulity", "Fret", "Tassel", "Lucubrate", "Provoke", "Implacable", "Latent", "Perilous", "Defer", "Adulteration", "Dissemble", "Odium", "Fervor", "Supplicate", "Heed", "Hirsute", "Chicanery", "Compunction", "Proliferate", "Perpetrate", "Truculence", "Luculent", "Nadir", "Propagation", "Scribble", "Myriad", "Skit", "Meticulous", "Glimmer", "Edible", "Tenuous", "Resigned", "Trudge", "Alleviate", "Goad", "Regicide", "Engender", "Extol", "Trite", "Paucity", "Redoubtable", "Stentorian", "Vigorous", "Pinch", "Prevaricate", "Encumbrance", "Quibble", "Enduring", "Nibble", "Peregrination", "Astute", "Stipple", "Inferno", "Incredulous", "Garner", "Finesse", "Epitome", "Fetter", "Abut", "Preponderance", "Timorous", "Meretricious", "Epithet", "Demagogue", "Fawn", "Squander", "Rivet", "Expatiate", "Cumbersome", "Specious", "Imperative", "Cling", "Decorum", "Deferential", "Deluge", "Inimical", "Reticent", "Euphoria", "Pith", "Surcharge", "Obtain", "Libel", "Irate", "Auxiliary", "Sober", "Dulcet", "Chauvinist", "Amalgamate", "Scurvy", "Cloture", "Connoisseur", "Ignominious", "Cursory", "Quixotic", "Accolade", "Sawdust", "Reproach", "Coddle", "Extort", "Eulogy", "Quotidian", "Inadvertent", "Stingy", "Cryptic", "Hone", "Stolid", "Sheath", "Ardor", "Hubris", "Conspicuous", "Smolder", "Urbane", "Recreancy", "Personable", "Combustion", "Pine", "Plumb", "Divergence", "Placate", "Veneer", "Odious", "Contumacious", "Abysmal", "Slake", "Venal", "Maul", "Libertine", "Allegiance", "Exculpate", "Taciturn", "Exorbitant", "Forbear", "Mendicant", "Paean", "Preternatural", "Tout", "Plead", "Buoyant", "Emaciate", "Plummet", "Effrontery", "Incorrigibility", "Ostracism", "Uncouth", "Intransigence", "Bigot", "Hew", "Regale", "Parsimonious", "Haughty", "Mollify", "Temperate", "Extenuate", "Pucker", "Ameliorate", "Petulant", "Procrastination", "Sponge", "Gloat", "Ford", "Endearing", "Collusion", "Incense", "Deter", "Condone", "Expurgate", "Mince", "Imperious", "Concord", "Retard", "Ignoble", "Pundit", "Unencumbered", "Conceal", "Endorse", "Nexus", "Divulge", "Equivocal", "Sophomoric", "Raffish", "Abscond", "Infuriate", "Fidget", "Impute", "Dwarf", "Molt", "Whimsical", "Heterogeneous", "Foolproof", "Propinquity", "Reiterate", "Discreet", "Fringe", "Rumple", "Disencumber", "Obtuse", "Brook", "Resort", "Misanthrope", "Idyll", "Succor", "Tepid", "Truce", "Inscrutable", "Apprehensive", "Jibe", "Ascribe", "Sycophant", "Audacious", "Splice", "Savor", "Elegy", "Apostate", "Barrage", "Perish", "Testiness", "Panegyric", "Sluggard", "Vain", "Cogent", "Nascent", "Inveterate", "Drowsiness", "Inured", "Hack", "Soot", "Fragile", "Coda", "Sedulous", "Poignant", "Insularity", "Introspection", "Extinct", "Foil", "Overweening", "Abacus", "Parenthesis", "Summarily", "Supine", "Canvass", "Refractory", "Misogynist", "Daunt", "Broach", "Gullible", "Vacillation", "Erudite", "Ambiguous", "Negligent", "Insinuate", "Inculcate", "Contentious", "Propitious", "Manacle", "Cordon", "Levee", "Bedizen", "Epicurean", "Pusillanimous", "Contiguous", "Antithetical", "Skiff", "Eddy", "Perfunctory", "Convoke", "Incipient", "Discredit", "Breach", "Prodigal", "Bellicose", "Precepts", "Sophisticated", "Sumptuous", "Morbid", "Forswear", "Rotund", "Credulous", "Falter", "Foment", "Aggravate", "Brazen", "Opprobrious", "Flamboyant", "Admonitory", "Spurious", "Salubrious", "Halcyon", "Nostrum", "Lope", "Innocuous", "Foible", "Amortize", "Dissolution", "Requite", "Sequence", "Effluvia", "Subpoena", "Exigency", "Impede", "Disseminate", "Implicit", "Eloquence", "Consequential", "Malinger", "Catalyst", "Extralegal", "Lavish", "Repast", "Quack", "Punctilious", "Foppish", "Bewilder", "Facile", "Ingest", "Dissent", "Tenacity", "Castigation", "Palpitate", "Feint", "Tortuous", "Fatuous", "Rejuvenation", "Transient", "Assuage", "Mundane", "Irksome", "August", "Iconoclast", "Tonic", "Sanity", "Blithe", "Scabbard", "Abide", "Husk", "Untoward", "Dilate", "Nonplused", "Turquoise", "Ambivalent", "Crush", "Affinity", "Reverent", "Suborn", "Chastened", "Gouge", "Transitory", "Teetotal", "Trickle", "Minatory", "Sidestep", "Superfluous", "Quell", "Precarious", "Disheveled", "Aberration", "Sordid", "Pest", "Maladroit", "Indistinct", "Impiety", "Reprobate", "Squalid", "Castigate", "Piquant", "Consume", "Lackluster", "Profundity", "Derogatory", "Flout", "Accretion", "Suppress", "Turpitude", "Terse", "Denouement", "Stigmatize", "Spurn", "Descry", "Rave", "Caustic", "Bask", "Jabber", "Ebullient", "Dogmatic", "Thrift", "Spear", "Bolster", "Glib", "Impassive", "Rescind", "Soar", "Relapse", "Ineptitude", "Dawdler", "Implosion", "Implicate", "Counterfeit", "Flax", "Inasmuch", "Hoax", "Neophyte", "Decree", "Austere", "Maudlin", "Prudence", "Weigh", "Tadpole", "Obviate", "Striated", "Gush", "Vanquish", "Benign", "Enmity", "Pedantic", "Verdant", "Sundry", "Volatile", "Tamper", "Fission", "Graze", "Abjure", "Agile", "Gnaw", "Tarnished", "Arboreal", "Divestiture", "Ferocity", "Epistle", "Conceit"
        };
        #endregion
    }
}