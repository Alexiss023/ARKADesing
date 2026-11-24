using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ARKADesingAPI.Models;

public partial class ArkaDbContext : DbContext
{
    public ArkaDbContext(DbContextOptions<ArkaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }

    public virtual DbSet<DetalleCotizacion> DetalleCotizacions { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Diseno> Disenos { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<HistorialEstadoPedido> HistorialEstadoPedidos { get; set; }

    public virtual DbSet<Insumo> Insumos { get; set; }

    public virtual DbSet<MovimientoInventario> MovimientoInventarios { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoInsumo> ProductoInsumos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CD54BC5A3865E89D");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__677F38F50873F781");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rtn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rtn");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.IdCotizacion).HasName("PK__Cotizaci__9713D1745DBD6493");

            entity.ToTable("Cotizacion");

            entity.Property(e => e.IdCotizacion).HasColumnName("id_cotizacion");
            entity.Property(e => e.DescripcionTrabajo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion_trabajo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.Vigencia).HasColumnName("vigencia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cotizacion_Cliente");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cotizacion_Usuario");
        });

        modelBuilder.Entity<DetalleCotizacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCotizacion).HasName("PK__Detalle___F3679436F193C25C");

            entity.ToTable("Detalle_Cotizacion");

            entity.Property(e => e.IdDetalleCotizacion).HasColumnName("id_detalle_cotizacion");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCotizacion).HasColumnName("id_cotizacion");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.NotaPersonalizacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nota_personalizacion");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdCotizacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetCot_Cotizacion");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetCot_Producto");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalle___4F1332DE30574672");

            entity.ToTable("Detalle_Pedido");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.NotaPersonalizacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nota_personalizacion");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPed_Pedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetPed_Producto");
        });

        modelBuilder.Entity<Diseno>(entity =>
        {
            entity.HasKey(e => e.IdDiseno).HasName("PK__Diseno__A58DA2FF2DF403AC");

            entity.ToTable("Diseno");

            entity.Property(e => e.IdDiseno).HasColumnName("id_diseno");
            entity.Property(e => e.ArchivoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("archivo_url");
            entity.Property(e => e.EstadoAprobacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente")
                .HasColumnName("estado_aprobacion");
            entity.Property(e => e.FechaSubida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_subida");
            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Version)
                .HasDefaultValue(1)
                .HasColumnName("version");

            entity.HasOne(d => d.IdDetalleNavigation).WithMany(p => p.Disenos)
                .HasForeignKey(d => d.IdDetalle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diseno_Detalle");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Disenos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diseno_Usuario");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PK__Entrega__017C2C8A56C86825");

            entity.ToTable("Entrega");

            entity.HasIndex(e => e.IdPedido, "UQ_Entrega_pedido").IsUnique();

            entity.Property(e => e.IdEntrega).HasColumnName("id_entrega");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdPedidoNavigation).WithOne(p => p.Entrega)
                .HasForeignKey<Entrega>(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entrega_Pedido");
        });

        modelBuilder.Entity<HistorialEstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__76E6C50244E87B7E");

            entity.ToTable("Historial_Estado_Pedido");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Observacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observacion");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.HistorialEstadoPedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistPed_Pedido");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialEstadoPedidos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistPed_Usuario");
        });

        modelBuilder.Entity<Insumo>(entity =>
        {
            entity.HasKey(e => e.IdInsumo).HasName("PK__Insumo__D4F202B1167D4C8E");

            entity.ToTable("Insumo");

            entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");
            entity.Property(e => e.Existencia)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("existencia");
            entity.Property(e => e.ExistenciaMinima)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("existencia_minima");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("unidad_medida");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Insumos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Insumo_Proveedor");
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C24055B8F6F");

            entity.ToTable("Movimiento_Inventario");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("motivo");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovInv_Insumo");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK_MovInv_Pedido");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovInv_Usuario");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__0941B074B1DF345D");

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Metodo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("metodo");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Referencia)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("referencia");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Pedido");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__6FF01489920250A6");

            entity.ToTable("Pedido");

            entity.HasIndex(e => e.IdCotizacion, "UX_Pedido_cotizacion")
                .IsUnique()
                .HasFilter("([id_cotizacion] IS NOT NULL)");

            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("recibido")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaEntregaEstimada).HasColumnName("fecha_entrega_estimada");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdCotizacion).HasColumnName("id_cotizacion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saldo");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Cliente");

            entity.HasOne(d => d.IdCotizacionNavigation).WithOne(p => p.Pedido)
                .HasForeignKey<Pedido>(d => d.IdCotizacion)
                .HasConstraintName("FK_Pedido_Cotizacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Usuario");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__FF341C0DE3323ABE");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioBase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_base");
            entity.Property(e => e.Tecnica)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("tecnica");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");
        });

        modelBuilder.Entity<ProductoInsumo>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdInsumo });

            entity.ToTable("Producto_Insumo");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");
            entity.Property(e => e.CantidadPorUnidad)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad_por_unidad");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.ProductoInsumos)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProdInsumo_Insumo");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoInsumos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProdInsumo_Producto");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__8D3DFE282B2C8C7B");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD84983201");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ_Usuario_correo").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
