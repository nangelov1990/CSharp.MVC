namespace Dashboard.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum RequestSolutionStep
    {
        [Display(Name = "Open Request")]
        OpenRequest,
        [Display(Name = "Identify the issues")]
        IdentifyIssues,
        [Display(Name = "Understand everyone\'s interests")]
        UnderstandInterests,
        [Display(Name = "List the possible solutions")]
        ListPossibleSolutions,
        [Display(Name = "Evaluate the options")]
        EvaluateOptions,
        [Display(Name = "Select an option or options")]
        SelectOption,
        [Display(Name = "Document the agreement")]
        DocumentAgreement,
        [Display(Name = "Delivery on agreed")]
        DeliverSolution,
        [Display(Name = "Close Request")]
        CloseRequest

    }
}
