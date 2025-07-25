import { Link } from "react-router-dom";
import { Button } from "@heroui/react";
import { Icon } from "@iconify-icon/react";
export default function IndexPage() {
  return (
    <main>
      <section className="flex flex-col items-center justify-center gap-8 py-12 md:py-20">
        <div className="w-full max-w-3xl bg-gradient-to-br from-green-50 to-white rounded-2xl shadow-lg p-8 flex flex-col items-center">
          <Icon
            icon="heroicons:building-library"
            width="64"
            height="64"
            className="text-green-600 mb-4"
          />
          <h1 className="text-4xl md:text-5xl font-bold text-green-700 mb-2 text-center">
            Panel de control
          </h1>
          <p className="text-lg text-green-700 mb-6 text-center max-w-xl">
            Bienvenido al dashboard de{" "}
            <span className="font-semibold text-green-600">
              Reserva con Enanos
            </span>
            . Desde aquí puedes gestionar proveedores y sus salas de forma
            rápida y sencilla.
          </p>
          <div className="flex flex-col sm:flex-row gap-4 w-full justify-center">
            <Button
              as={Link}
              to="/providers"
              color="success"
              variant="solid"
              className="w-full flex items-center gap-2 px-8 py-4 text-lg font-semibold shadow"
            >
              <Icon icon="heroicons:users" width="28" height="28" />
              Gestionar proveedores
            </Button>
            <Button
              as={Link}
              to="/dashboard"
              color="success"
              variant="flat"
              className="w-full flex items-center gap-2 px-8 py-4 text-lg font-semibold text-green-700 hover:text-green-900"
            >
              <Icon icon="heroicons:home" width="28" height="28" />
              Ir al dashboard
            </Button>
          </div>
        </div>
      </section>
    </main>
  );
}
