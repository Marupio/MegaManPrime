public abstract class SourceDataObjHeader : DataObjHeader, ISourceDataObjMeta {
    // Currently ISourceDataObjMeta is empty, but if it had stuff, it would start here
    public SourceDataObjHeader(string name, IObjRegistry parent = null) : base(name, parent) {}
    public SourceDataObjHeader(SourceDataObjHeader obj) : base(obj) {}
    public SourceDataObjHeader() {}
}
